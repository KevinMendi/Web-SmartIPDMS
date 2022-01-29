import http from "../http-common";

class DashboardDataService {
    GetProjectsCount(userId) {
        return http.get(`/IpdmsFile/count/projects/user/${userId}`);
    }

    GetInventionCount(userId) {
        return http.get(`/IpdmsFile/count/invention/user/${userId}`);
    }

    GetUtilityModelCount(userId) {
        return http.get(`/IpdmsFile/count/utility-model/user/${userId}`);
    }

    GetFinishedProjectsCount(userId) {
        return http.get(`/IpdmsFile/count/projects/finished/user/${userId}`);
    }

    GetOfficeActionUpdateList(userId) {
        return http.get(`/IpdmsFile/dashboard/uploaded/office-action/user/${userId}`);
    }

    GetRecentProjects(userId) {
        return http.get(`/IpdmsFile/dashboard/project/recent/user/${userId}`);
    }

    GetAgents() {
        return http.get(`/IpdmsFile/dashboard/agents`);
    }


}

export default new DashboardDataService();