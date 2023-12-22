import { Roles } from "../types/roles";

export interface User {
  idRolUser:string
  userId: string;
  roleTypeId: string;
  companyId: string | null;
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
