namespace Functional.Models;

public class Either<TLeft, TRight>
{
    private readonly TLeft _left;
    private readonly TRight _right;
    private readonly bool isLeft;


    public Either(TLeft left)
    {
        _left = left;
        _right = default!;
        isLeft = true;
    }

    public Either(TRight right)
    {
        _right = right;
        _left = default!;
        isLeft = false;
    }
    public static implicit operator Either<TLeft, TRight>(TLeft left) => new Either<TLeft, TRight>(left);
    public static implicit operator Either<TLeft, TRight>(TRight right) => new Either<TLeft, TRight>(right);

    /// <summary>
    /// Match the either to a value of type T.
    /// </summary>
    /// <typeparam name="T">The type to match to.</typeparam>
    /// <param name="left">The function to apply if the either is a left.</param>
    /// <param name="right">The function to apply if the either is a right.</param>
    /// <returns>The value of type T.</returns>
    public T Match<T>(Func<TLeft, T> left, Func<TRight, T> right) => 
        isLeft ? left(_left) : right(_right);

}
