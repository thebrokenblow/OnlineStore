namespace OnlineStore.UnitTests.Extensions;

public static class ListExtensions
{
    public static T Second<T>(this List<T> list)
    {
        if (list == null || list.Count < 2)
        {
            throw new ArgumentException("List does not contain a second element.");
        }

        return list[1];
    }

    public static T Third<T>(this List<T> list)
    {
        if (list == null || list.Count < 3)
        {
            throw new ArgumentException("List does not contain a third element.");
        }

        return list[2];
    }
}