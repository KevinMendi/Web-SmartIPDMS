import http from "../http-common";

class LookUpDataService {
    GetApplicationTypes() {
        return http.get("/ApplicationTypes");
    }

}

export default new LookUpDataService();