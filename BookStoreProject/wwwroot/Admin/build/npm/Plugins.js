'use strict'

const /Admin/plugins = [
  / jQuery
  {
    from: 'node_modules/jquery/dist',
    to: '/Admin/plugins/jquery'
  },
  / Popper
  {
    from: 'node_modules/popper.js/dist',
    to: '/Admin/plugins/popper'
  },
  / Bootstrap
  {
    from: 'node_modules/bootstrap/Admin/dist/js',
    to: '/Admin/plugins/bootstrap/js'
  },
  / Font Awesome
  {
    from: 'node_modules/@fortawesome/fontawesome-free/css',
    to: '/Admin/plugins/fontawesome-free/css'
  },
  {
    from: 'node_modules/@fortawesome/fontawesome-free/webfonts',
    to: '/Admin/plugins/fontawesome-free/webfonts'
  },
  / overlayScrollbars
  {
    from: 'node_modules/overlayscrollbars/js',
    to: '/Admin/plugins/overlayScrollbars/js'
  },
  {
    from: 'node_modules/overlayscrollbars/css',
    to: '/Admin/plugins/overlayScrollbars/css'
  },
  / Chart.js
  {
    from: 'node_modules/chart.js/Admin/dist/',
    to: '/Admin/plugins/chart.js'
  },
  / jQuery UI
  {
    from: 'node_modules/jquery-ui-/Admin/dist/',
    to: '/Admin/plugins/jquery-ui'
  },
  / Flot
  {
    from: 'node_modules/flot/Admin/dist/es5/',
    to: '/Admin/plugins/flot'
  },
  {
    from: 'node_modules/flot/source/',
    to: '/Admin/plugins/flot/Admin/plugins'
  },
  / Summernote
  {
    from: 'node_modules/summernote/Admin/dist/',
    to: '/Admin/plugins/summernote'
  },
  / Bootstrap Slider
  {
    from: 'node_modules/bootstrap-slider/Admin/dist/',
    to: '/Admin/plugins/bootstrap-slider'
  },
  {
    from: 'node_modules/bootstrap-slider/Admin/dist/css',
    to: '/Admin/plugins/bootstrap-slider/css'
  },
  / Bootstrap Colorpicker
  {
    from: 'node_modules/bootstrap-colorpicker/Admin/dist/js',
    to: '/Admin/plugins/bootstrap-colorpicker/js'
  },
  {
    from: 'node_modules/bootstrap-colorpicker/Admin/dist/css',
    to: '/Admin/plugins/bootstrap-colorpicker/css'
  },
  / Tempusdominus Bootstrap 4
  {
    from: 'node_modules/tempusdominus-bootstrap-4/Admin/build/js',
    to: '/Admin/plugins/tempusdominus-bootstrap-4/js'
  },
  {
    from: 'node_modules/tempusdominus-bootstrap-4/Admin/build/css',
    to: '/Admin/plugins/tempusdominus-bootstrap-4/css'
  },
  / Moment
  {
    from: 'node_modules/moment/min',
    to: '/Admin/plugins/moment'
  },
  {
    from: 'node_modules/moment/locale',
    to: '/Admin/plugins/moment/locale'
  },
  / FastClick
  {
    from: 'node_modules/fastclick/lib',
    to: '/Admin/plugins/fastclick'
  },
  / Date Range Picker
  {
    from: 'node_modules/daterangepicker/',
    to: '/Admin/plugins/daterangepicker'
  },
  / DataTables
  {
    from: 'node_modules/pdfmake/build',
    to: '/Admin/plugins/pdfmake'
  },
  {
    from: 'node_modules/jszip/dist',
    to: '/Admin/plugins/jszip'
  },
  {
    from: 'node_modules/datatables.net/js',
    to: '/Admin/plugins/datatables'
  },
  {
    from: 'node_modules/datatables.net-bs4/js',
    to: '/Admin/plugins/datatables-bs4/js'
  },
  {
    from: 'node_modules/datatables.net-bs4/css',
    to: '/Admin/plugins/datatables-bs4/css'
  },
  {
    from: 'node_modules/datatables.net-autofill/js',
    to: '/Admin/plugins/datatables-autofill/js'
  },
  {
    from: 'node_modules/datatables.net-autofill-bs4/js',
    to: '/Admin/plugins/datatables-autofill/js'
  },
  {
    from: 'node_modules/datatables.net-autofill-bs4/css',
    to: '/Admin/plugins/datatables-autofill/css'
  },
  {
    from: 'node_modules/datatables.net-buttons/js',
    to: '/Admin/plugins/datatables-buttons/js'
  },
  {
    from: 'node_modules/datatables.net-buttons-bs4/js',
    to: '/Admin/plugins/datatables-buttons/js'
  },
  {
    from: 'node_modules/datatables.net-buttons-bs4/css',
    to: '/Admin/plugins/datatables-buttons/css'
  },
  {
    from: 'node_modules/datatables.net-colreorder/js',
    to: '/Admin/plugins/datatables-colreorder/js'
  },
  {
    from: 'node_modules/datatables.net-colreorder-bs4/js',
    to: '/Admin/plugins/datatables-colreorder/js'
  },
  {
    from: 'node_modules/datatables.net-colreorder-bs4/css',
    to: '/Admin/plugins/datatables-colreorder/css'
  },
  {
    from: 'node_modules/datatables.net-fixedcolumns/js',
    to: '/Admin/plugins/datatables-fixedcolumns/js'
  },
  {
    from: 'node_modules/datatables.net-fixedcolumns-bs4/js',
    to: '/Admin/plugins/datatables-fixedcolumns/js'
  },
  {
    from: 'node_modules/datatables.net-fixedcolumns-bs4/css',
    to: '/Admin/plugins/datatables-fixedcolumns/css'
  },
  {
    from: 'node_modules/datatables.net-fixedheader/js',
    to: '/Admin/plugins/datatables-fixedheader/js'
  },
  {
    from: 'node_modules/datatables.net-fixedheader-bs4/js',
    to: '/Admin/plugins/datatables-fixedheader/js'
  },
  {
    from: 'node_modules/datatables.net-fixedheader-bs4/css',
    to: '/Admin/plugins/datatables-fixedheader/css'
  },
  {
    from: 'node_modules/datatables.net-keytable/js',
    to: '/Admin/plugins/datatables-keytable/js'
  },
  {
    from: 'node_modules/datatables.net-keytable-bs4/js',
    to: '/Admin/plugins/datatables-keytable/js'
  },
  {
    from: 'node_modules/datatables.net-keytable-bs4/css',
    to: '/Admin/plugins/datatables-keytable/css'
  },
  {
    from: 'node_modules/datatables.net-responsive/js',
    to: '/Admin/plugins/datatables-responsive/js'
  },
  {
    from: 'node_modules/datatables.net-responsive-bs4/js',
    to: '/Admin/plugins/datatables-responsive/js'
  },
  {
    from: 'node_modules/datatables.net-responsive-bs4/css',
    to: '/Admin/plugins/datatables-responsive/css'
  },
  {
    from: 'node_modules/datatables.net-rowgroup/js',
    to: '/Admin/plugins/datatables-rowgroup/js'
  },
  {
    from: 'node_modules/datatables.net-rowgroup-bs4/js',
    to: '/Admin/plugins/datatables-rowgroup/js'
  },
  {
    from: 'node_modules/datatables.net-rowgroup-bs4/css',
    to: '/Admin/plugins/datatables-rowgroup/css'
  },
  {
    from: 'node_modules/datatables.net-rowreorder/js',
    to: '/Admin/plugins/datatables-rowreorder/js'
  },
  {
    from: 'node_modules/datatables.net-rowreorder-bs4/js',
    to: '/Admin/plugins/datatables-rowreorder/js'
  },
  {
    from: 'node_modules/datatables.net-rowreorder-bs4/css',
    to: '/Admin/plugins/datatables-rowreorder/css'
  },
  {
    from: 'node_modules/datatables.net-scroller/js',
    to: '/Admin/plugins/datatables-scroller/js'
  },
  {
    from: 'node_modules/datatables.net-scroller-bs4/js',
    to: '/Admin/plugins/datatables-scroller/js'
  },
  {
    from: 'node_modules/datatables.net-scroller-bs4/css',
    to: '/Admin/plugins/datatables-scroller/css'
  },
  {
    from: 'node_modules/datatables.net-searchpanes/js',
    to: '/Admin/plugins/datatables-searchpanes/js'
  },
  {
    from: 'node_modules/datatables.net-searchpanes-bs4/js',
    to: '/Admin/plugins/datatables-searchpanes/js'
  },
  {
    from: 'node_modules/datatables.net-searchpanes-bs4/css',
    to: '/Admin/plugins/datatables-searchpanes/css'
  },
  {
    from: 'node_modules/datatables.net-select/js',
    to: '/Admin/plugins/datatables-select/js'
  },
  {
    from: 'node_modules/datatables.net-select-bs4/js',
    to: '/Admin/plugins/datatables-select/js'
  },
  {
    from: 'node_modules/datatables.net-select-bs4/css',
    to: '/Admin/plugins/datatables-select/css'
  },

  / Fullcalendar
  {
    from: 'node_modules/fullcalendar/',
    to: '/Admin/plugins/fullcalendar'
  },
  / icheck bootstrap
  {
    from: 'node_modules/icheck-bootstrap/',
    to: '/Admin/plugins/icheck-bootstrap'
  },
  / inputmask
  {
    from: 'node_modules/inputmask/Admin/dist/',
    to: '/Admin/plugins/inputmask'
  },
  / ion-rangeslider
  {
    from: 'node_modules/ion-rangeslider/',
    to: '/Admin/plugins/ion-rangeslider'
  },
  / JQVMap (jqvmap-novulnerability)
  {
    from: 'node_modules/jqvmap-novulnerability/Admin/dist/',
    to: '/Admin/plugins/jqvmap'
  },
  / jQuery Mapael
  {
    from: 'node_modules/jquery-mapael/js/',
    to: '/Admin/plugins/jquery-mapael'
  },
  / Raphael
  {
    from: 'node_modules/raphael/',
    to: '/Admin/plugins/raphael'
  },
  / jQuery Mousewheel
  {
    from: 'node_modules/jquery-mousewheel/',
    to: '/Admin/plugins/jquery-mousewheel'
  },
  / jQuery Knob
  {
    from: 'node_modules/jquery-knob-chif/Admin/dist/',
    to: '/Admin/plugins/jquery-knob'
  },
  / pace-progress
  {
    from: 'node_modules/@lgaitan/pace-progress/Admin/dist/',
    to: '/Admin/plugins/pace-progress'
  },
  / Select2
  {
    from: 'node_modules/select2/Admin/dist/',
    to: '/Admin/plugins/select2'
  },
  {
    from: 'node_modules/@ttskch/select2-bootstrap4-theme/Admin/dist/',
    to: '/Admin/plugins/select2-bootstrap4-theme'
  },
  / Sparklines
  {
    from: 'node_modules/sparklines/source/',
    to: '/Admin/plugins/sparklines'
  },
  / SweetAlert2
  {
    from: 'node_modules/sweetalert2/Admin/dist/',
    to: '/Admin/plugins/sweetalert2'
  },
  {
    from: 'node_modules/@sweetalert2/theme-bootstrap-4/',
    to: '/Admin/plugins/sweetalert2-theme-bootstrap-4'
  },
  / Toastr
  {
    from: 'node_modules/toastr/Admin/build/',
    to: '/Admin/plugins/toastr'
  },
  / jsGrid
  {
    from: 'node_modules/jsgrid/dist',
    to: '/Admin/plugins/jsgrid'
  },
  {
    from: 'node_modules/jsgrid/demos/db.js',
    to: '/Admin/plugins/jsgrid/demos/db.js'
  },
  / flag-icon-css
  {
    from: 'node_modules/flag-icon-css/css',
    to: '/Admin/plugins/flag-icon-css/css'
  },
  {
    from: 'node_modules/flag-icon-css/flags',
    to: '/Admin/plugins/flag-icon-css/flags'
  },
  / bootstrap4-duallistbox
  {
    from: 'node_modules/bootstrap4-duallistbox/dist',
    to: '/Admin/plugins/bootstrap4-duallistbox/'
  },
  / filterizr
  {
    from: 'node_modules/filterizr/dist',
    to: '/Admin/plugins/filterizr/'
  },
  / ekko-lightbox
  {
    from: 'node_modules/ekko-lightbox/dist',
    to: '/Admin/plugins/ekko-lightbox/'
  },
  / bootstrap-switch
  {
    from: 'node_modules/bootstrap-switch/dist',
    to: '/Admin/plugins/bootstrap-switch/'
  },
  / jQuery Validate
  {
    from: 'node_modules/jquery-validation/Admin/dist/',
    to: '/Admin/plugins/jquery-validation'
  },
  / bs-custom-file-input
  {
    from: 'node_modules/bs-custom-file-input/Admin/dist/',
    to: '/Admin/plugins/bs-custom-file-input'
  },
  / bs-stepper
  {
    from: 'node_modules/bs-stepper/Admin/dist/',
    to: '/Admin/plugins/bs-stepper'
  },
  / CodeMirror
  {
    from: 'node_modules/codemirror/lib/',
    to: '/Admin/plugins/codemirror'
  },
  {
    from: 'node_modules/codemirror/addon/',
    to: '/Admin/plugins/codemirror/addon'
  },
  {
    from: 'node_modules/codemirror/keymap/',
    to: '/Admin/plugins/codemirror/keymap'
  },
  {
    from: 'node_modules/codemirror/mode/',
    to: '/Admin/plugins/codemirror/mode'
  },
  {
    from: 'node_modules/codemirror/theme/',
    to: '/Admin/plugins/codemirror/theme'
  },
  / dropzonejs
  {
    from: 'node_modules/dropzone/Admin/dist/',
    to: '/Admin/plugins/dropzone'
  },
  / uPlot
  {
    from: 'node_modules/uplot/Admin/dist/',
    to: '/Admin/plugins/uplot'
  }
]

module.exports = /Admin/plugins
