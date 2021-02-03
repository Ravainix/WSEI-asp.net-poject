import * as api from "./api";
import { commentsApiURL } from "./routes";

export const getAll = (id) => api.get(commentsApiURL(id));

export const create = (comment) => api.post(commentsApiURL(), comment);
