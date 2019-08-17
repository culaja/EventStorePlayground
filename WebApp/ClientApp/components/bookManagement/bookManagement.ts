import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import axios from 'axios';

@Component
export default class BookManagementComponent extends Vue {
    bookToAddId = '';
    bookToAddName = '';
    bookToAddYearOfPrint = null;
    
    isSuccess = false;
    isError = false;
    errorMessage = '';

    addBook() {
        axios.post('http://localhost:7230/api/Book/Add' + 
            '?id=' + this.bookToAddId + 
            '&name=' + this.bookToAddName +
            '&yearOfPrint=' + this.bookToAddYearOfPrint)
            .then(r => this.handleSuccess())
            .catch(e => this.handleError(e.response.data));
    }
    
    handleSuccess() {
        this.isSuccess = true;
        this.isError = false;
        this.errorMessage = '';
    }
    
    handleError(errorMessage: string) {
        this.isSuccess = false;
        this.isError = true;
        this.errorMessage = errorMessage;
    }
}
