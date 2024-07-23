export interface UserModel {
  userID: number | null;
  firstName: string;
  lastName: string;
  email: string;
  username: string;
}

export interface UserRegistrationModel {
  email: string;
  password: string;
}