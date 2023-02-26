namespace Functional.Models;

public static class OptionExtensions
{
    /// <summary>
    /// Map the current type of an Option object to a new type.
    /// </summary>
    /// <typeparam name="T">The current type of the Option object.</typeparam>
    /// <typeparam name="TResult">The new type of the Option object.</typeparam>
    /// <param name="option">The Option object to map.</param>
    /// <param name="map">The function to map the current type to the new type.</param>
    /// <returns>A new Option object with the new type.</returns>
    public static Option<TResult> Map<T, TResult>(this Option<T> option, Func<T, TResult> map) =>
        option is Some<T> some 
            ? new Some<TResult>(map(some.Content)) 
            : new None<TResult>();

    /// <summary>
    /// Perform filtering on an Option object.
    /// </summary>
    /// <typeparam name="T">The type of the Option object.</typeparam>
    /// <param name="option">The Option object to filter.</param>
    /// <param name="predicate">The predicate to filter the Option object.</param>
    /// <returns>A new Option object with the same type.</returns>
    public static Option<T> Filter<T>(this Option<T> option, Func<T, bool> predicate) =>
        option is Some<T> some && !predicate(some.Content)
            ? new None<T>()
            : option;

    /// <summary>
    /// Return the content of the Option object if it is a Some object, otherwise return the substitute value.
    /// </summary>
    /// <typeparam name="T">The type of the Option object.</typeparam>
    /// <param name="option">The Option object to reduce.</param>
    /// <param name="Substitute">The substitute value to return if the Option object is a None object.</param>
    /// <returns>The content of the Option object if it is a Some object, otherwise the substitute value.</returns>
    public static T Reduce<T>(this Option<T> option, T Substitute) =>
        option is Some<T> some
            ? some.Content
            : Substitute;

    /// <summary>
    /// Return the content of the Option object if it is a Some object, otherwise return the substitute value.
    /// </summary>
    /// <typeparam name="T">The type of the Option object.</typeparam>
    /// <param name="option">The Option object to reduce.</param>
    /// <param name="Substitute">The substitute Function to invoke if the Option object is a None object.</param>
    /// <returns>The content of the Option object if it is a Some object, otherwise the substitute value.</returns>
    public static T Reduce<T>(this Option<T> option, Func<T> Substitute) =>
        option is Some<T> some
            ? some.Content
            : Substitute();

    /// <summary>
    /// Convert an IEnumerable of T to an Option of T using LINQ SingleOrDefault.
    /// </summary>
    /// <typeparam name="T">The type of the IEnumerable.</typeparam>
    /// <param name="enumerable">The IEnumerable to convert.</param>
    /// <returns>An Option of T.</returns>
    public static Option<T> SingleOrDefault<T>(this IEnumerable<T> enumerable) =>
        enumerable.Select(opt => opt.Optional()).SingleOrDefault(None.Value);
}
