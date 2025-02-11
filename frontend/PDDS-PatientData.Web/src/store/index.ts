import { createStore } from "vuex";
import modules from './modules';

export default createStore({
  state: {},
  mutations: {},
  actions: {},
  modules,
  strict: process.env.NODE_ENV !== 'production'
});