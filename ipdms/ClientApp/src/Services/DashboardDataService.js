import http from "../http-common";

class DashboardDataService {
    GetProjectsCount(userId) {
        return http.get(`/IpdmsFile/count/projects/user/${userId}`);
    }

    GetInventionCount(userId, roleId) {
        return http.get(`/IpdmsFile/count/invention/user/${userId}/role/${roleId}`);
    }

    GetUtilityModelCount(userId, roleId) {
        return http.get(`/IpdmsFile/count/utility-model/user/${userId}/role/${roleId}`);
    }

    GetFinishedProjectsCount(userId, roleId) {
        return http.get(`/IpdmsFile/count/projects/finished/user/${userId}/role/${roleId}`);
    }

    GetConvertedProjectsCount(userId, roleId) {
        return http.get(`/IpdmsFile/count/projects/converted/user/${userId}/role/${roleId}`);
    }

    //
    GetInventionInProgress(userId, roleId) {
        return http.get(`/IpdmsFile/invention/user/${userId}/role/${roleId}`);
    }

    GetUtilityModelInProgress(userId, roleId) {
        return http.get(`/IpdmsFile/utility-model/user/${userId}/role/${roleId}`);
    }

    GetFinishedProjects(userId, roleId) {
        return http.get(`/IpdmsFile/projects/finished/user/${userId}/role/${roleId}`);
    }

    GetConvertedProjects(userId, roleId) {
        return http.get(`/IpdmsFile/projects/converted/user/${userId}/role/${roleId}`);
    }
    //

    GetOfficeActionUpdateList(userId, roleId) {
        return http.get(`/IpdmsFile/dashboard/uploaded/office-action/user/${userId}/role/${roleId}`);
    }

    GetRecentProjects(userId, roleId) {
        return http.get(`/IpdmsFile/dashboard/project/recent/user/${userId}/role/${roleId}`);
    }

    GetAgents() {
        return http.get(`/IpdmsFile/dashboard/agents`);
    }

    GetProjectWithDueCount(userId, roleId) {
        return http.get(`/IpdmsFile/project/due/user/${userId}/role/${roleId}`);
    }

    GetProjectWithDueMonth(userId, roleId) {
        return http.get(`/IpdmsFile/project/due/month/user/${userId}/role/${roleId}`);
    }

    GetProjectWithDueWeek(userId, roleId) {
        return http.get(`/IpdmsFile/project/due/week/user/${userId}/role/${roleId}`);
    }

    GetProjectWithDueToday(userId, roleId) {
        return http.get(`/IpdmsFile/project/due/today/user/${userId}/role/${roleId}`);
    }

    GetProjectSummaryData(userId, roleId, year) {
        return http.get(`/IpdmsFile/project/user/${userId}/role/${roleId}/year/${year}`);
    }

}

export default new DashboardDataService();