import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import axios from 'axios';
import toast from "../app/infrastructure/toast";

@Component
export default class BookManagementComponent extends Vue {
    bookToAddId = '';
    bookToAddName = '';
    bookToAddYearOfPrint = null;

    addBook() {
        axios.post('http://localhost:7230/api/Book/Add' + 
            '?id=' + this.bookToAddId + 
            '&name=' + this.bookToAddName +
            '&yearOfPrint=' + this.bookToAddYearOfPrint)
            .then(r => {
                toast.onSuccess(r, `Book ${this.bookToAddId}' added to library.`);
                this.prepareFieldsForNextEntry(r);
            });
    }
    
    prepareFieldsForNextEntry(response: any) {
        if (response) {
            this.bookToAddId = '';
            this.bookToAddName = '';
            this.bookToAddYearOfPrint = null;
        }
    }
}
