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

}

export default new LookUpDataService();