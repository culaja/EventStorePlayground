import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import axios from 'axios';
import toast from "../app/infrastructure/toast";

@Component
export default class BookManagementComponent extends Vue {
    bookToLendId = '';
    userId = '';

    lendBook() {
        axios.post('http://localhost:7230/api/Lending/LendBook' + 
            '?bookToLendId=' + this.bookToLendId + 
            '&userId=' + this.userId)
            .then(r => {
                toast.onSuccess(r, `Book '${this.bookToLendId}' is borrower to user '${this.userId}'.`);
                this.prepareFieldsForNextEntry(r);
            });
    }
    
    prepareFieldsForNextEntry(response: any) {
        if (response) {
            this.bookToLendId = '';
            this.userId = '';
        }
    }
}
