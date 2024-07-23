export interface PatientModel {
  patientID: number;
  firstName: string;
  lastName: string;
  address1: string | null;
  address2: string | null;
  city: string | null;
  state: string | null;
  postalCode: string | null;
  email: string | null;
  primaryPhone: string;
  dateOfBirth: string;
}