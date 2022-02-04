import http from "../http-common";

class UserDataService {
    GetAllUsers() {
        return http.get("/IpdmsUsers");
    }

    GetUserById(id) {
        return http.get(`/IpdmsUsers/${id}`);
    }

    SaveUser(data) {
        return http.post("/IpdmsUsers", data);
    }

    CheckCredential(data) {
        return http.post("/IpdmsUsers/signin", data);
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
    GetUserListTable() {
        return http.get("/IpdmsUsers/GetUserListTable");
    }
}

export default new UserDataService();