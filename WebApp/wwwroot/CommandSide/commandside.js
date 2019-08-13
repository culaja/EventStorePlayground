new Vue({
    el: '#command_side_app',
    data: {
        bookToAddId: '',
        bookToAddName: '',
        yearOfPrint: 2010,
        info: null
    },
    methods: {
        addNewBook: function () {
            axios
                .post('http://localhost:7320/api/Book/Add?id=' + this.bookToAddId + '&name=' + this.bookToAddName + '&yearOfPrint=' + this.yearOfPrint)
                .then(response => (this.info = 'Book ' + this.bookToAddId + ' added!'))
        .catch(error => this.info = error)
        }
    }
})