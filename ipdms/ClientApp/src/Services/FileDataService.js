import http from "../http-common";

class FileDataService {
    AnalyzeImage(data) {
        return http.post("/IpdmsFile/analyze/image", data);
    } 

    SaveProject(data) {
        return http.post("/IpdmsFile", data);
    } 

    GetProjectList() {
        return http.get("/IpdmsFile/projects");
    }

}

export default new FileDataService();