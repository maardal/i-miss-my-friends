export type LovedOne = {
    id: string,
    name: string,
    relationship: Relationship,
    lastHangout: Date
}

type Relationship = "family" | "friend"
