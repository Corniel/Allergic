using System;
using System.Reflection;

namespace Allergic
{
    /// <summary>Reflector helper.</summary>
    /// <remarks>
    /// This is relatively slow, as it does not cache it's result. Not the way to
    /// use reflection in production code. For this purpose is nice to have more
    /// readable code though.
    /// </remarks>
    public static class Ctor
    {
        /// <summary>Creates an object using an none-public constructor with arguments.</summary>
        public static T New<T>(params object[] args)
        {
            var ctors = typeof(T).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var ctor in ctors)
            {
                var pars = ctor.GetParameters();
                if (ParametersMatch(args, pars))
                {
                    return (T)ctor.Invoke(args);
                }
            }
            throw new NotSupportedException("Could not find a constructor that matches the arguments.");
        }

        private static bool ParametersMatch(object[] args, ParameterInfo[] pars)
        {
            if (pars.Length != args.Length) { return false; }

            for (var i = 0; i < pars.Length; i++)
            {
                if (args[i] != null &&
                    !args[i].GetType().IsAssignableFrom(pars[i].ParameterType))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
