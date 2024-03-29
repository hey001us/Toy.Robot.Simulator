﻿namespace Toy.Robot.Simulator.Helper
{
    using System.Linq;

    public static class EnumExtensions
    {
        public static T Next<T>(this T v) where T : struct
        {
            return System.Enum.GetValues(v.GetType()).Cast<T>().Concat(new[] { default(T) }).SkipWhile(e => !v.Equals(e)).Skip(1).First();
        }

        public static T Previous<T>(this T v) where T : struct
        {
            return System.Enum.GetValues(v.GetType()).Cast<T>().Concat(new[] { default(T) }).Reverse().SkipWhile(e => !v.Equals(e)).Skip(1).First();
        }

    }
}
