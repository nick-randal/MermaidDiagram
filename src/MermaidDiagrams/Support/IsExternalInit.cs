// ReSharper disable once CheckNamespace
#if NET5_0_OR_GREATER
#else
namespace System.Runtime.CompilerServices
{
	// this is needed to enable the record feature in .NET framework and .NET core <= 3.1 projects
	internal static class IsExternalInit { }
}
#endif