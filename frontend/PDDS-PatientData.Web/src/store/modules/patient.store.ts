import { ActionContext } from "vuex";
import { PatientModel } from "@/models/PatientModel";
import patientService from "@/api/PatientService";

export interface State {
  patients: PatientsState;
}

type Context = ActionContext<PatientsState, State>;

export interface PatientsState {
  items: Array<PatientModel>;
};

const state: PatientsState = { 
  items: Array<PatientModel>()
};

const getters = { 
  getPatients(): Array<PatientModel> {
    return state.items
  },
};

const mutations = {
  setPatients: function (state: PatientsState, patients: Array<PatientModel>): void {
    state.items = patients;
  }
};

const actions = {
  async getPatients(context: Context): Promise<Array<PatientModel>> {
    return await new patientService().getPatients()
      .then(function (response) {
        const responseData = response.data.patients;
        const patients: PatientModel[] = responseData.map((p: any) => ({
          patientID: p.patientID,
          firstName: p.firstName,
          lastName: p.lastName,
          address1: p.address1,
          address2: p.address2,
          city: p.city,
          state: p.state,
          postalCode: p.postalCode,
          email: p.email,
          primaryPhone: p.primaryPhone,
          dateOfBirth: p.dateOfBirth
        }));
        context.commit("setPatients", patients);
        return context.state.items;
      }).catch((err) => {
        throw err;
      });
  },
  async getPatient(context: Context, id: number): Promise<PatientModel> {
    return await new patientService().getById(id)
      .then(function (response) {
        const data = response.data;
        return {
          patientID: data.patientID,
          firstName: data.firstName,
          lastName: data.lastName,
          address1: data.address1,
          address2: data.address2,
          city: data.city,
          state: data.state,
          postalCode: data.postalCode,
          email: data.email,
          primaryPhone: data.primaryPhone,
          dateOfBirth: data.dateOfBirth
        }
      }).catch((err) => {
        throw err;
      });
  },
  async addPatient(context: Context, data: PatientModel) {
    await new patientService().add(JSON.stringify(data))
      .then(function (response) {
        const responseData = response.data;
        console.log(responseData);
      }).catch((err) => {
        throw err;
      });
  },
  async updatePatient(context: Context, data: PatientModel) {
    await new patientService().update(data.patientID, JSON.stringify(data))
      .then(function (response) {
        const responseData = response.data;
      }).catch((err) => {
        throw err;
      });
  },
  async deletePatient(context: Context, id: number) {
    await new patientService().delete(id)
      .then(function (response) {
        const responseData = response.data;
      }).catch((err) => {
        throw err;
      });
  }
};

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
};
