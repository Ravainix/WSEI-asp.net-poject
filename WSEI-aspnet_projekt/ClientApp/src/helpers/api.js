import axios from "axios";
import AuthorizeService from "../components/api-authorization/AuthorizeService";

const apiCall = async (url, method, body) =>
  axios({
    method: method,
    url: url,
    data: body,
    headers: {
      Authorization: `Bearer ${await AuthorizeService.getAccessToken()}`,
    },
  })
    .then((response) => response.data)
    .catch((error) => {
      console.error(error.response);
      return error;
    });

export const get = async (url) => apiCall(url, "GET");

export const post = async (url, body) => apiCall(url, "POST", body);

export const put = async (url, body) => apiCall(url, "PUT", body);

export const destroy = async (url) => apiCall(url, "DELETE");
