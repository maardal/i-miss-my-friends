type Relationship = 'family' | 'friend';

export type LovedOne = {
  id: string;
  name: string;
  relationship: Relationship;
  lastHangout: Date;
};

export type Hangout = {
  id: string;
  date: string;
  lovedOneId: string;
};

export type CreateLovedoneRequest = Omit<LovedOne, "id" | "lastHangout">;
