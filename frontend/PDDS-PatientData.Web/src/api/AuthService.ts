import PatientDataServieClient from "./serviceclients/PatientDataServiceClient"
import BaseService from "./BaseService";

export default class AuthService extends BaseService {
  constructor () {
    super(PatientDataServieClient, "auth");
  }

  login(data: string) {
    return this.execute("post", "/login", null, data);
  }

  logout(data: string) {
    return this.execute("post", "/logout", null, data);
  }

  register(data: string) {
    return this.execute("post", "/register", null, data);
  }
} 
