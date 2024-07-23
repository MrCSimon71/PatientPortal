import { ActionContext } from "vuex";
import authService from "@/api/AuthService";
import { SessionModel } from "@/models/SessionModel";
import { LoginModel } from "@/models/LoginModel";
import { LogoutModel } from "@/models/LogoutModel";
import moment from "moment";

export interface SessionState {
  session: SessionModel | null;
};

export interface State {
  session: SessionModel;
}

type Context = ActionContext<SessionState, State>;

const state: SessionState = {
  session: {
    startDateTime: new Date,
    lastActivtyDateTime: null,
    token: '',
    isValid: false
  } as SessionModel
};

const getters = {
  getToken(state: SessionState): string {
    if (state.session !== null) {
      return state.session.token;
    }
    return '';
  }
};

const mutations = {
  setSession: function (state: SessionState, session: SessionModel) {
    state.session = session;
  },
  setSessionActivity: function (state: SessionState) {
    if (state.session !== null) {
      state.session.lastActivtyDateTime = new Date();
    }
  },
  logOut: function (state: SessionState) {
    state.session = null
  }
};

const actions = {
  async initialize(context: Context) {
    //console.log('Initializing Auth Store...')

    const sessionString = localStorage.getItem('session');

    if (sessionString !== null) {
      const session = JSON.parse(sessionString) as SessionModel;
      context.commit('setSession', session);
      checkSessionActivity();
    }
  },
  async logIn(context: Context, data: LoginModel) {
    await new authService().login(JSON.stringify(data))
      .then(response => {
        const currentDateTime = new Date();

        const session: SessionModel = {
          startDateTime: currentDateTime,
          lastActivtyDateTime: currentDateTime,
          token: response.data.token,
          isValid: true
        };

        context.commit('setSession', session);
        context.commit('User/setUser', response.data, { root: true });

        localStorage.setItem("session", JSON.stringify(session));

      }).catch(error => {
        context.commit('setSession', null);
        context.commit('User/setUser', null, { root: true });
        throw error;
      });
  },
  async logOut(context: Context, data: LogoutModel) {
    await new authService().logout(JSON.stringify(data))
      .then(response => {
        clearSession(context);
      }).catch(error => {
        console.log(error);
        clearSession(context);
      });
  },
  async resetPassword(context: Context, data: any) {
    clearSession(context);
  },
  async savePassword(context: Context, data: any) {
    clearSession(context);
  },
  async setSessionActivity(context: Context) {
    context.commit('setSessionActivity')
  },
  async isSessionValid(context: Context) {
    return checkSessionActivity();
  }
};

function checkSessionActivity() : boolean {

  if (state.session === null) return false;

  const currentDateTime = moment(new Date().toISOString());
  const sessionLastActivityDateTime = moment(state.session.lastActivtyDateTime);
  const duration = moment.duration(currentDateTime.diff(sessionLastActivityDateTime));
  const minutes = duration.asMinutes();

  state.session.isValid = minutes <= process.env.VUE_APP_SESSION_CHECK_INTERVAL;

  //console.log(`${currentDateTime.format('LTS')}|${sessionLastActivityDateTime.format('LTS')}|${minutes.toPrecision(4)}|${state.session.isValid}`);

  return state.session.isValid;
}

function clearSession(context: Context) {
  localStorage.removeItem("session");
  context.commit('logOut', null);
  context.commit('User/setUser', null, { root: true });
}

export default {
     namespaced: true,
     state,
     getters,
     actions,
     mutations
};