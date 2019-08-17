import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import axios from 'axios';

@Component
export default class BookManagementComponent extends Vue {
    bookToAddId = '';
    bookToAddName = '';
    bookToAddYearOfPrint = 2010;
    info = '';

    addBook() {
        axios.post('http://localhost:7230/api/Book/Add' + 
            '?id=' + this.bookToAddId + 
            '&name=' + this.bookToAddName +
            '&yearOfPrint=' + this.bookToAddYearOfPrint)
            .then(r => this.info = 'Book ' + this.bookToAddId + ' added.')
            .catch(e => this.info = e.response.data);
    }
}
