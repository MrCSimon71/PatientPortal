import axios, { AxiosError, AxiosResponse } from "axios";

// Interceptor for responses
const responseInterceptor = (response: AxiosResponse): AxiosResponse => {
  switch (response.status) {
    case 200:
      break;
    default:
      break;
  }
  return response;
}

const errorInterceptor = (error: AxiosError | Error): Promise<AxiosError> => {
  let errorMsg = "";

  if (axios.isAxiosError(error)) {

    const { message } = error;
    const { statusText, status } = error.response as AxiosResponse ?? {};

    switch (status) {
      case 400: {
        errorMsg = status + " : " + message;
        break;
      }
      case 401: {
        // "Login required"
        errorMsg = message + " : " + JSON.stringify(statusText);
        break;
      }
      case 403: {
        // "Permission denied"
        break;
      }
      case 404: {
        // "Invalid request"
        errorMsg = message;
        break;
      }
      case 500: {
        // "Server error"
        break;
      }
      default: {
        errorMsg = statusText + " : " + message;
        break;
      }
    }
  } else {
    errorMsg = "Unknown Error: " + error.message;
  }

  return Promise.reject(errorMsg);
};

export default {
     responseInterceptor,
     errorInterceptor
}