import http from "../http-common";

class LookUpDataService {
    GetApplicationTypes() {
        return http.get("/ApplicationTypes");
    }

    GetDocumentById(id) {
        return http.get(`/Documents/${id}`);
    }

    DeleteDocumentById(id, data){
        return http.put(`/Documents/${id}`, data);
    }

    GetOfficeActions() {
        return http.get("/OfficeActions");
    }

    SaveOfficeAction(data) {
        return http.post("/OfficeActions", data);
    }

    GetProjectById(id) {
        return http.get(`/Documents/project/${id}`);
    }

}

export default new LookUpDataService();