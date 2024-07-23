import axios from "axios";
import interceptors from "../interceptors/response.interceptor";

const PatientDataServieClient = axios.create({
     baseURL: process.env.VUE_APP_PDDS_PATIENT_SERVICE_URL,
     headers: {
          'Content-Type': 'application/json',
    }
});

PatientDataServieClient.interceptors.response.use(
  interceptors.responseInterceptor,
  interceptors.errorInterceptor);

export default PatientDataServieClient;
