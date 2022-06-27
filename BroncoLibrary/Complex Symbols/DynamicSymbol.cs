using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public abstract class DynamicSymbol : ISymbol
    {
        //I know this seems janky, and it kinda is, but Microsoft does it with Funcs so that makes it okay right?
        protected delegate ISymbol EvalVarArgs(ISymbol[] args);

        protected delegate ISymbol EvalArgs();
        protected delegate ISymbol EvalArgs<in T>(T arg) 
            where T : ISymbol;
        protected delegate ISymbol EvalArgs<in T1, in T2>(T1 arg1, T2 arg2) 
            where T1 : ISymbol where T2 : ISymbol;
        protected delegate ISymbol EvalArgs<in T1, in T2, in T3>(T1 arg1, T2 arg2, T3 arg3)
            where T1 : ISymbol where T2 : ISymbol where T3 : ISymbol;
        protected delegate ISymbol EvalArgs<in T1, in T2, in T3, in T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            where T1 : ISymbol where T2 : ISymbol where T3 : ISymbol where T4 : ISymbol;
        protected delegate ISymbol EvalArgs<in T1, in T2, in T3, in T4, in T5>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            where T1 : ISymbol where T2 : ISymbol where T3 : ISymbol where T4 : ISymbol where T5 : ISymbol;
        protected delegate ISymbol EvalArgs<in T1, in T2, in T3, in T4, in T5, in T6>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
            where T1 : ISymbol where T2 : ISymbol where T3 : ISymbol where T4 : ISymbol where T5 : ISymbol where T6 : ISymbol;

        private class ArgumentHolder : ISymbol
        {
            protected Type[] _argTypes;
            protected Delegate _evalDelegate;
            protected ISymbol[] _arguments;
            protected DynamicSymbol _callee;

            public ArgumentHolder(Delegate evalDelegate, Type[] argTypes, ISymbol[] args, DynamicSymbol callee)
            {
                _argTypes = argTypes;
                _evalDelegate = evalDelegate;
                _arguments = args;
                _callee = callee;
            }

            public virtual ISymbol Evaluate()
            {
                SetArgs();

                var flatArgs = new ISymbol[_argTypes.Length];

                for(int i = 0; i < flatArgs.Length; i++)
                {
                    ISymbol flat = _arguments[i].FlattenTo(_argTypes[i]);

                    if (flat == null) throw new ArgumentException($"Provided argument did not flatten to required {_argTypes[i]}");
                    
                    flatArgs[i] = flat;
                }

                return (ISymbol) _evalDelegate.DynamicInvoke(flatArgs);
            }

            public ISymbol GetArgument(int index) => _arguments[index];

            protected void SetArgs()
            {
                foreach (int i in _callee._argumentLookup.Keys)
                    _callee._argumentLookup[i].SetPointer(_arguments[i]);
            }
        }

        private class VarArgHolder : ArgumentHolder
        {
            public VarArgHolder(EvalVarArgs eval, ISymbol[] args, DynamicSymbol callee) : base(eval, new Type[args.Length], args, callee) 
            {
                Array.Fill(_argTypes, typeof(ISymbol));
            }

            public override ISymbol Evaluate()
            {
                SetArgs();
                return (ISymbol) ((EvalVarArgs) _evalDelegate)(_arguments);
            }
        }

        private List<(Type[], Delegate)> _evaluationList = new();
        private Dictionary<int, SymbolVariable> _argumentLookup = new();
        private EvalVarArgs _varArgEvaluation = null;

        public ISymbol Evaluate()
            => Argue(ISymbol.EmptyArgs);

        public ISymbol GetArgument(int index)
        {
            SymbolVariable arg;
            if(!_argumentLookup.TryGetValue(index, out arg))
            {
                arg = new SymbolVariable($"Arg{index}");
                _argumentLookup[index] = arg;
            }

            return arg;
        }

        public ISymbol Argue(ISymbol[] args)
        {
            (Type[], Delegate) eval;

            if (FindEvaluation(args, out eval))
                return new ArgumentHolder(eval.Item2, eval.Item1, args, this);
            if(_varArgEvaluation != null)
                return new VarArgHolder(_varArgEvaluation, args, this);

            throw new ArgumentException("Arguments do not match any evaluation");
        }

        protected bool FindEvaluation(ISymbol[] args, out (Type[], Delegate) outEval)
        {
            foreach(var eval in _evaluationList)
            {
                if (eval.Item1.Length != args.Length) continue;

                outEval = eval;
                return true;
            }

            outEval = (null, null);
            return false;
        }

        protected void AddEvaluationDelegate(Delegate eval)
            => _evaluationList.Add((eval.GetType().GenericTypeArguments, eval));

        protected void AddEvaluation(EvalVarArgs eval)
            => _varArgEvaluation = eval;

        protected void AddEvaluation(EvalArgs eval)
            => AddEvaluationDelegate(eval);

        protected void AddEvaluation<T1>(EvalArgs<T1> eval)
            where T1 : ISymbol
            => AddEvaluationDelegate(eval);

        protected void AddEvaluation<T1, T2>(EvalArgs<T1, T2> eval)
            where T1 : ISymbol where T2 : ISymbol
            => AddEvaluationDelegate(eval);

        protected void AddEvaluation<T1, T2, T3>(EvalArgs<T1, T2, T3> eval)
            where T1 : ISymbol where T2 : ISymbol where T3 : ISymbol
            => AddEvaluationDelegate(eval);

        protected void AddEvaluation<T1, T2, T3, T4>(EvalArgs<T1, T2, T3, T4> eval)
            where T1 : ISymbol where T2 : ISymbol where T3 : ISymbol where T4 : ISymbol
            => AddEvaluationDelegate(eval);

        protected void AddEvaluation<T1, T2, T3, T4, T5>(EvalArgs<T1, T2, T3, T4, T5> eval)
            where T1 : ISymbol where T2 : ISymbol where T3 : ISymbol where T4 : ISymbol where T5 : ISymbol
            => AddEvaluationDelegate(eval);

        protected void AddEvaluation<T1, T2, T3, T4, T5, T6>(EvalArgs<T1, T2, T3, T4, T5, T6> eval)
            where T1 : ISymbol where T2 : ISymbol where T3 : ISymbol where T4 : ISymbol where T5 : ISymbol where T6 : ISymbol
            => AddEvaluationDelegate(eval);

    }
}
