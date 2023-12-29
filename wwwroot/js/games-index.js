$(document).ready(function () {
    $('.js-delete').on('click', function () {
        var btn = $(this);
        //console.log($(this).parents('tr').fadeOut())
     const sw = Swal.mixin({
            customClass: {
                confirmButton: "btn btn-success",
                cancelButton: "btn btn-danger"
            },
            buttonsStyling: false
        });

        sw.fire({
            title: "Are you sure?",
            text: "You won't be able to revert this!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: "Yes, delete it!",
            cancelButtonText: "No, cancel!",
            reverseButtons: true
        }).then((result) => {

            console.log(result)
            if (result.isConfirmed) {
                $.ajax({
                    url: `/Games/Delete/${btn.data('id')}`,
                    method: 'DELETE',
                    success: function () {
                        sw.fire({
                            title: "Deleted!",
                            text: "Your Game has been deleted.",
                            icon: "success"
                        });

                        btn.parents('tr').fadeOut();
                    },
                    error: function () {
                        sw.fire({
                            title: "Cancelled",
                            text: "Your imaginary Game is safe :)",
                            icon: "error"
                        });
                    }
                })
            }

        });

        

    })
})