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

    GetProjectById(projectId) {
        return http.get(`/IpdmsFile/project/${projectId}`);
    }

    UpdateProject(id, data) {
        return http.put(`/IpdmsFile/${id}`, data);
    }

    
}

export default new FileDataService();