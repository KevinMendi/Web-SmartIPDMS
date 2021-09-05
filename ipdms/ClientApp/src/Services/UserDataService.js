import http from "../http-common";

class UserDataService {
    getAllUsers() {
        return http.get("/IpdmsUsers");
    }

    GetUserById(id) {
        return http.get(`/IpdmsUsers/${id}`);
    }

    SaveUser(data) {
        return http.post("/IpdmsUsers", data);
    }

    UpdateUser(id, data) {
        return http.put(`/IpdmsUsers/${id}`, data);
    }

    DeleteUserById(id) {
        return http.delete(`/IpdmsUsers/${id}`);
    }

    FindUserById(id) {
        return http.get(`/IpdmsUsers?id=${id}`);
    }
}

export default new UserDataService();