using Immf.Models;

public static class EnumTools
{
    public static Relationship RelationshipMapper(string enumAsString)
    {
        Relationship returnEnum = Relationship.Friend;
        switch (enumAsString)
        {
            case "Friend":
                returnEnum = Relationship.Friend;
                break;
            case "Family":
                returnEnum = Relationship.Family;
                break;
        }
        return returnEnum;
    }

    public static string EnumListPrettified()
    {
        var relationships = "";
        foreach (string s in Enum.GetNames(typeof(Relationship)))
        {
            relationships += $" {s}";
        }
        return relationships;
    }
}
