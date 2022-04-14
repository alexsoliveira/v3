FROM node:alpine AS build

WORKDIR /app

# COPY . .

RUN npm install
RUN npm build --prod

FROM nginx

WORKDIR /app

COPY --from=build /app/dist/cartorio /usr/share/nginx/html
