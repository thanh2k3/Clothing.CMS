function InitDataTable(idTable) {
    $(idTable).DataTable({
        //"lengthMenu": [[50, 75, 150, -1], [50, 75, 150, "All"]],
        "responsive": true, "lengthChange": false, "autoWidth": false,
        "buttons": ["copy", "csv", "excel", "pdf", "print"],
        "language": {
            "sProcessing": "Đang xử lý...",
            "sLengthMenu": "Xem _MENU_ mục",
            "sZeroRecords": "Không tìm thấy dòng nào phù hợp",
            "sInfo": "Đang xem từ _START_ đến _END_ trong tổng số _TOTAL_ mục",
            "sInfoEmpty": "Đang xem 0 đến 0 trong tổng số 0 mục",
            "sInfoFiltered": "(được lọc từ _MAX_ mục)",
            "sInfoPostFix": "",
            "sSearch": "Tìm kiếm:",
            "sUrl": "",
            "oPaginate": {
                "sFirst": "Đầu",
                "sPrevious": "Trước",
                "sNext": "Tiếp",
                "sLast": "Cuối"
            }
        },
        //paging: true,
        //serverSide: true,
        //listAction: {
        //    ajaxFunction: "",
        //    inputFilter: function () {
        //        return "";
        //    }
        //},

        //"columnDefs": [
        //    //{ "orderable": false, "targets": [0] },
        //    { "targets": 0, "visible": false },
        //],
        //"ordering": false
        "aaSorting": [] //disable sort default
        //https://www.codegrepper.com/code-examples/javascript/disable+default+sorting+in+datatable
    });
    //.buttons().container().appendTo(idTable + '_wrapper .col-md-6:eq(0)');
};