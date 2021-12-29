const { createProxyMiddleware } = require("http-proxy-middleware");

module.exports = function (app) {
  const appProxy = createProxyMiddleware({
    target: "https://localhost:7238",
    secure: false,
    changeOrigin: true,
  });

  app.use("/api", appProxy);
};
