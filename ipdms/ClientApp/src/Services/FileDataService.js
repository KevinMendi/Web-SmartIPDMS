import http from "../http-common";

class FileDataService {
    AnalyzeImage(data) {
        return http.post("/IpdmsFile/analyze/image", data);
    } 

    SaveProject(data) {
        return http.post("/IpdmsFile", data);
    } 

    GetProjectList(userId, roleId, year) {
        return http.get(`/IpdmsFile/projects/user/${userId}/role/${roleId}/year/${year}`);
    }

    GetDocumentListByProjectId(projectId) {
        return http.get(`/IpdmsFile/project/document/${projectId}`);
    }

    
}

export default new FileDataService();