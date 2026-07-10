// JavaScript handlers for Sales Orders page
// All actions are implemented here so they can later be wired to API via AJAX.

(function () {
    function init() {
        const btnAdd = document.getElementById('btnAddNew');
        const btnExport = document.getElementById('btnExport');
        const btnSearch = document.getElementById('btnSearch');
        const keyword = document.getElementById('soKeyword');
        const orderDate = document.getElementById('soOrderDate');

        if (btnAdd) btnAdd.addEventListener('click', onAddNew);
        if (btnExport) btnExport.addEventListener('click', onExport);
        if (btnSearch) btnSearch.addEventListener('click', onSearch);

        // row actions
        document.querySelectorAll('.btn-view').forEach(b => b.addEventListener('click', onView));
        document.querySelectorAll('.btn-edit').forEach(b => b.addEventListener('click', onEdit));
        document.querySelectorAll('.btn-delete').forEach(b => b.addEventListener('click', onDelete));

        // pagination links
        document.querySelectorAll('.pagination a.page-link').forEach(a => a.addEventListener('click', onPageLink));
    }

    function onAddNew(e) {
        // navigate to create page
        window.location.href = '/Create';
    }

    function onExport(e) {
        // placeholder: navigate to export
        window.location.href = '/Export';
    }

    function onSearch(e) {
        const kw = document.getElementById('soKeyword').value;
        const od = document.getElementById('soOrderDate').value;
        const qs = new URLSearchParams();
        if (kw) qs.set('keyword', kw);
        if (od) qs.set('orderDate', od);
        qs.set('Page', '1');
        qs.set('pageSize', '5');
        window.location.href = '?' + qs.toString();
    }

    function onView(e) {
        const id = e.currentTarget.getAttribute('data-id');
        const so = e.currentTarget.getAttribute('data-sonumber');
        // view mode: pass view=true
        window.location.href = `/Create?id=${id}&view=true`;
    }

    function onEdit(e) {
        const id = e.currentTarget.getAttribute('data-id');
        window.location.href = `/Create?id=${id}`;
    }

    function onDelete(e) {
        const id = e.currentTarget.getAttribute('data-id');
        const so = e.currentTarget.getAttribute('data-sonumber');
        if (!confirm(`Hapus order ${so} ?, tindakan ini tidak dapat dibatalkan.`)) return;
        // submit the hidden form to invoke server handler (later change to AJAX)
        const form = document.getElementById('deleteForm-' + id);
        if (form) {
            form.submit();
        }
    }

    function onPageLink(e) {
        // allow normal navigation but ensure query params are preserved via JS if desired
        // prevent default to implement client-side navigation if needed
        // For now let anchor work as-is but ensure query param Page is used
    }

    // init on DOM ready
    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', init);
    } else {
        init();
    }
})();
