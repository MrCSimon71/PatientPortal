export interface SessionModel {
  startDateTime: Date;
  lastActivtyDateTime: Date | null;
  token: string;
  isValid: boolean;
}