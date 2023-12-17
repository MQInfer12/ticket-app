import { Roles } from "../types/roles";

export interface User {
  userId: string;
  roleTypeId: string;
  companyId: string;
  roleName: Roles;
  userName: string;
  password: string;
  companyName: string;
  companyAddress: string;
  companyState: boolean;
  personName: string;
  personLastName: string | null;
  personLast: string | null;
}
