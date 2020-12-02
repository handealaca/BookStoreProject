'use strict'

const /Admin/plugins = [
  // AdminLTE Dist
  {
    from: '/Admin/dist/css/',
    to: 'docs/assets/css/'
  },
  {
    from: '/Admin/dist/js/',
    to: 'docs/assets/js/'
  },
  // jQuery
  {
    from: 'node_modules/jquery/Admin/dist/',
    to: 'docs/assets/Admin/plugins/jquery/'
  },
  // Popper
  {
    from: 'node_modules/popper.js/Admin/dist/',
    to: 'docs/assets/Admin/plugins/popper/'
  },
  // Bootstrap
  {
    from: 'node_modules/bootstrap/Admin/dist/js/',
    to: 'docs/assets/Admin/plugins/bootstrap/js/'
  },
  // Font Awesome
  {
    from: 'node_modules/@fortawesome/fontawesome-free/css/',
    to: 'docs/assets/Admin/plugins/fontawesome-free/css/'
  },
  {
    from: 'node_modules/@fortawesome/fontawesome-free/webfonts/',
    to: 'docs/assets/Admin/plugins/fontawesome-free/webfonts/'
  },
  // overlayScrollbars
  {
    from: 'node_modules/overlayscrollbars/js/',
    to: 'docs/assets/Admin/plugins/overlayScrollbars/js/'
  },
  {
    from: 'node_modules/overlayscrollbars/css/',
    to: 'docs/assets/Admin/plugins/overlayScrollbars/css/'
  }
]

module.exports = /Admin/plugins
