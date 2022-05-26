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
        public static readonly ISymbol[] EmptyArgs = new ISymbol[0];

        //I know this seems janky, and it kinda is, but Microsoft does it with Funcs so that makes it okay right?
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
            private Delegate _eval;
            private ISymbol[] _arguments;

            public ArgumentHolder(Delegate eval, ISymbol[] args)
            {
                _eval = eval;
                _arguments = args;
            }

            public ISymbol Evaluate()
                => (ISymbol)_eval.DynamicInvoke(_arguments);
        }

        private List<(Type[], Delegate)> _evaluationList = new();

        public ISymbol Evaluate()
            => Argue(EmptyArgs);

        public ISymbol Argue(ISymbol[] args)
        {
            Delegate eval;

            if (!FindEvaluation(ref args, out eval))
                throw new ArgumentException("Arguments do not match any evaluation");

            return new ArgumentHolder(eval, args);
        }

        protected bool FindEvaluation(ref ISymbol[] args, out Delegate outEval)
        {
            foreach(var eval in _evaluationList)
            {
                if (!EvalMatches(eval.Item1, ref args)) continue;

                outEval = eval.Item2;
                return true;
            }

            outEval = null;
            return false;
        }

        private bool EvalMatches(Type[] eval, ref ISymbol[] args)
        {
            if (eval.Length != args.Length) return false;

            ISymbol[] flatArgs = new ISymbol[args.Length];

            for (int i = 0; i < args.Length; i++)
            {
                ISymbol flat = args[i].FlattenTo(eval[i]);
                if (flat == null)
                    return false;
                flatArgs[i] = flat;
            }

            for(int i = 0; i < args.Length; i++)
            {
                args[i] = flatArgs[i];
            }

            return true;
        }

        protected void AddEvaluationDelegate(Delegate eval)
            => _evaluationList.Add((eval.GetType().GenericTypeArguments, eval));

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
