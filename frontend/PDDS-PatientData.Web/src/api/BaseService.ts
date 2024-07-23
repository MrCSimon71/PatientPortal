import { AxiosInstance } from "axios";
import QueryParams from "./QueryParams";
import store from "../store";

export default class BaseService {
  private serviceClient: AxiosInstance;
  private apiController: string;

  constructor (serviceClient: AxiosInstance, apiController: string) {
    this.serviceClient = serviceClient;
    this.apiController = apiController;
  }

  async execute(method: string, resource: string, queryParams: QueryParams<string> | null, data: string | null) {

    resource = resource !== null ? resource : "";
    const queryString = this.BuildQueryString(queryParams);

    const token = store.getters['Auth/getToken'];

    return await this.serviceClient.request({
      method,
      url: this.apiController + resource + queryString,
      data,
      headers: {
        Authorization: `Bearer ${token}`
      }
    }).then(response => {
        return response;
    }).catch(error => {
        throw error;
    });
  }

  getAll() {
      return this.execute("get", "/", null, null);
  }

  getById(id: number | string) {
      return this.execute("get", `/${id}`, null, null);
  }

  add(data: string) {
      return this.execute("post", "/", null, data);
  }

  update(id: number | string, data: string) {
      return this.execute("put", `/${id}`, null, data);
  }

  delete(id: number | string) {
      return this.execute("delete", `/${id}`, null, null);
  }

  private BuildQueryString(queryParams: QueryParams<string> | null): string {
    let queryString = '';

    if (queryParams !== null) {
      for (const key in queryParams) {
        if (Object.prototype.hasOwnProperty.call(queryParams, key)) {
          if (queryParams[key] !== '') {
            const queryParam = `${key}=${queryParams[key]}`;
            queryString += queryString !== '' ? `&${queryParam}` : `${queryParam}`;
          }
        }
      }
      queryString = `?${queryString}`
    }

    return queryString;
  }
}
