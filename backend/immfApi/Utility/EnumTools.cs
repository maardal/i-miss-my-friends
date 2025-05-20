using immfApi.Models;

public static class EnumTools
{
    public static Relationship MapStringToEnumRelationship(string enumAsString)
    {
        Relationship returnEnum = enumAsString.ToLower() switch
        {
            "friend" => Relationship.Friend,
            "family" => Relationship.Family,
            _ => Relationship.Friend
        };
        return returnEnum;
    }

    public static bool IsValidRelationship(string relationship)
    {
        return Enum.GetNames(typeof(Relationship)).ToList().Exists(relation => string.Equals(relation.ToLower(), relationship.ToLower()));
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
