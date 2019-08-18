import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import axios from 'axios';
import toast from "../app/infrastructure/toast";

@Component
export default class UserManagementComponent extends Vue {
    userToAddId = '';
    userToAddFullName = '';

    addUser() {
        axios.post('http://localhost:7230/api/User/Add' + 
            '?id=' + this.userToAddId + 
            '&fullName=' + this.userToAddFullName)
            .then(r => {
                this.prepareFieldsForNextEntry(r);
                toast.onSuccess(r, `User ${this.userToAddId}' added to library.`);
            });
    }
    
    prepareFieldsForNextEntry(response: any) {
        if (response) {
            this.userToAddId = '';
            this.userToAddFullName = '';
        }
    }
}
