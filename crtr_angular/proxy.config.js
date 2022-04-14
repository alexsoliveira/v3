const proxy = [
  {
    context: '/api',
    target: 'http://localhost:5000',
    secure: false,
    changeOrigin: true,
    pathRewrite: { '^/api': '' }
  }
];

module.exports = proxy;