import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import axios from 'axios';

@Component
export default class BookManagementComponent extends Vue {
    bookToAddId = '';
    bookToAddName = '';
    bookToAddYearOfPrint = null;

    addBook() {
        axios.post('http://localhost:7230/api/Book/Add' + 
            '?id=' + this.bookToAddId + 
            '&name=' + this.bookToAddName +
            '&yearOfPrint=' + this.bookToAddYearOfPrint);
    }
}
