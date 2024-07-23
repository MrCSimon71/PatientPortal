import { ActionContext } from "vuex";
import userService from "@/api/UserService";
import { UserModel, UserRegistrationModel } from "@/models/UserModel";
import { PagingAttributesModel } from "@/models/PagingAttributesModel";
import QueryParams from "@/api/QueryParams";

type Context = ActionContext<UsersState, State>;

export interface UsersState {
  user: UserModel;
  users: Array<UserModel>;
  pagingAttributes: PagingAttributesModel;
};

export interface State {
  users: UsersState;
};

const state: UsersState = {
  user: {} as UserModel,
  users: Array<UserModel>(),
  pagingAttributes: {} as PagingAttributesModel
};

const getters = { 
  getUser(state: UsersState): UserModel {
    return state.user;
  },
  getUsers(state: UsersState): Array<UserModel> {
    return state.users;
  },
  getPagingAttributes(state: UsersState): PagingAttributesModel {
    return state.pagingAttributes;
  }
};

const mutations = {
  setUser: function (state: UsersState, user: UserModel): void {
    state.user = user;
  },
  setUsers: function (state: UsersState, users: Array<UserModel>): void {
    state.users = users;
  },
  setPagingAttributes: function (state: UsersState, pagingAttributes: PagingAttributesModel): void {
    state.pagingAttributes = pagingAttributes;
  }
};

const actions = {
  async getUsers(context: Context, queryParams: QueryParams<string>): Promise<Array<UserModel>> {
    return await new userService().getUsers(queryParams)
      .then(function (response) {

        const pagingAttributes: PagingAttributesModel = response.data.meta;
        const responseData = response.data.users;

        const users: UserModel[] = responseData.map((u: any) => ({
          userID: u.userID,
          firstName: u.firstName,
          lastName: u.lastName,
          email: u.email,
          username: u.username,
        }));

        context.commit("setUsers", users);
        context.commit("setPagingAttributes", pagingAttributes);

        return context.state.users;

      }).catch((err) => {
        throw err;
      });
  },
  async getUser(context: Context, id: string): Promise<UserModel> {
    return await new userService().getById(id)
      .then(function (response) {
        const responseData = response.data;
        const user: UserModel = {
          userID: responseData.userID,
          firstName: responseData.firstName,
          lastName: responseData.lastName,
          email: responseData.email,
          username: responseData.username,
        };
        context.commit("setUser", user);
        return user;
      }).catch((err) => {
        throw err;
      });
  },
  async addUser(context: Context, data: UserModel): Promise<number> {
    return await new userService().add(JSON.stringify(data))
      .then(function (response) {
        return response.data.userId;
      }).catch((err) => {
        throw err;
      });
  },
  async updateUser(context: Context, data: UserModel) {
    await new userService().update(data.userID as number, JSON.stringify(data))
      .then(function (response) {
        const responseData = response.data;
        context.commit("setUser", data);
      }).catch((err) => {
        throw err;
      });
  },
  async deleteUser(context: Context, id: string) {
    await new userService().delete(id)
      .then(function (response) {
        const responseData = response.data;
      }).catch((err) => {
        throw err;
      });
  },
  async registerUser(context: Context, data: UserRegistrationModel) {
    await new userService().registerUser(JSON.stringify(data))
      .then(function (result) {
        return result.data;
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
}