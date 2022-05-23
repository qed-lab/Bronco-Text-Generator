﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroncoLibrary
{
    public abstract class Symbol
    {
        public static readonly Symbol[] EmptyArgs = new Symbol[0];

        //I know this seems janky, and it kinda is, but Microsoft does it with Funcs so that makes it okay right?
        protected delegate Symbol EvalArgs();
        protected delegate Symbol EvalArgs<in T>(T arg) 
            where T : Symbol;
        protected delegate Symbol EvalArgs<in T1, in T2>(T1 arg1, T2 arg2) 
            where T1 : Symbol where T2 : Symbol;
        protected delegate Symbol EvalArgs<in T1, in T2, in T3>(T1 arg1, T2 arg2, T3 arg3)
            where T1 : Symbol where T2 : Symbol where T3 : Symbol;
        protected delegate Symbol EvalArgs<in T1, in T2, in T3, in T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            where T1 : Symbol where T2 : Symbol where T3 : Symbol where T4 : Symbol;
        protected delegate Symbol EvalArgs<in T1, in T2, in T3, in T4, in T5>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            where T1 : Symbol where T2 : Symbol where T3 : Symbol where T4 : Symbol where T5 : Symbol;
        protected delegate Symbol EvalArgs<in T1, in T2, in T3, in T4, in T5, in T6>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
            where T1 : Symbol where T2 : Symbol where T3 : Symbol where T4 : Symbol where T5 : Symbol where T6 : Symbol;

        private class ArgEqualityComparer : IEqualityComparer<Type[]>
        {
            public bool Equals(Type[]? x, Type[]? y)
            {
                if (x.Length != y.Length) return false;

                for (int i = 0; i < x.Length; i++)
                {
                    if (x[i] != y[i]) return false;
                }

                return true;
            }

            public int GetHashCode([DisallowNull] Type[] obj)
            {
                return obj.Length.GetHashCode() + obj.Length != 0 ? obj[0].GetHashCode() : 0;
            }
        }

        private Dictionary<Type[], Delegate> _evaluationLookup = new(new ArgEqualityComparer());

        public Symbol Evaluate(Symbol[] args)
        {
            var argTypes = new Type[args.Length];

            for (int i = 0; i < args.Length; i++)
                argTypes[i] = args[i].GetType();

            if (!_evaluationLookup.ContainsKey(argTypes))
                throw new ArgumentException("Arguments do not match any evaluation");

            return (Symbol) _evaluationLookup[argTypes].DynamicInvoke(args);
        }

        public String EvaluateString(Symbol[] args)
        {
            if (this is ITerminal) return ((ITerminal)this).Value;

            try
            {
                return Evaluate(args).EvaluateString(args);
            }
            catch
            {
                throw new InvalidOperationException("This symbol does not have a string representation for those arguments");
            }
        }

        protected void addEvaluationDelegate(Delegate eval)
            => _evaluationLookup.Add(eval.GetType().GenericTypeArguments, eval);

        protected void addEvaluation(EvalArgs eval)
            => addEvaluationDelegate(eval);

        protected void addEvaluation<T1>(EvalArgs<T1> eval)
            where T1 : Symbol
            => addEvaluationDelegate(eval);

        protected void addEvaluation<T1, T2>(EvalArgs<T1, T2> eval)
            where T1 : Symbol where T2 : Symbol
            => addEvaluationDelegate(eval);

        protected void addEvaluation<T1, T2, T3>(EvalArgs<T1, T2, T3> eval)
            where T1 : Symbol where T2 : Symbol where T3 : Symbol
            => addEvaluationDelegate(eval);

        protected void addEvaluation<T1, T2, T3, T4>(EvalArgs<T1, T2, T3, T4> eval)
            where T1 : Symbol where T2 : Symbol where T3 : Symbol where T4 : Symbol
            => addEvaluationDelegate(eval);

        protected void addEvaluation<T1, T2, T3, T4, T5>(EvalArgs<T1, T2, T3, T4, T5> eval)
            where T1 : Symbol where T2 : Symbol where T3 : Symbol where T4 : Symbol where T5 : Symbol
            => addEvaluationDelegate(eval);

        protected void addEvaluation<T1, T2, T3, T4, T5, T6>(EvalArgs<T1, T2, T3, T4, T5, T6> eval)
            where T1 : Symbol where T2 : Symbol where T3 : Symbol where T4 : Symbol where T5 : Symbol where T6 : Symbol
            => addEvaluationDelegate(eval);

    }
}
