import PatientDataServiceClient from "./serviceclients/PatientDataServiceClient"
import BaseService from "./BaseService";

export default class PatientService extends BaseService {
  constructor () {
    super(PatientDataServiceClient, "patients");
  }

  getPatients() {
    //console.log('[CustomerService.ts] => getCustomers');
    return this.execute("get", "/", null, null);
  }

  //getById(id: number) : number{
  //  return this.getById(id);
  //}

} 
