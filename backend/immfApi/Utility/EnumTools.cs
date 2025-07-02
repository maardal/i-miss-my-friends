using immfApi.Models;

public static class EnumTools
{
    public static Relationship MapStringToEnumRelationship(string enumAsString)
    {
        Relationship returnEnum = Relationship.Friend;
        switch (enumAsString.ToLower())
        {
            case "friend":
                returnEnum = Relationship.Friend;
                break;
            case "family":
                returnEnum = Relationship.Family;
                break;
        }
        return returnEnum;
    }

    public static string MapEnumToStringRelationship(Relationship relationship)
    {
        string defaultReturn = relationship switch
        {
            Relationship.Family => "family",
            Relationship.Friend => "friend",
            _ => "unknown relationship",
        };
        return defaultReturn;

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
