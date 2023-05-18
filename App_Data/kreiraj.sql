CREATE TABLE users (
  id SERIAL PRIMARY KEY,
  username VARCHAR(255),
  email VARCHAR(255),
  password VARCHAR(255)
);


CREATE TABLE userstory (
  id SERIAL PRIMARY KEY,
  title VARCHAR(255),
  description TEXT,
  userid INT,
  CONSTRAINT "FK_UserStory_User" FOREIGN KEY (userid) REFERENCES users (id)
);


CREATE TABLE task (
  id SERIAL PRIMARY KEY,
  title VARCHAR(255),
  description TEXT,
  userstoryid INT,
  CONSTRAINT "FK_Task_UserStory" FOREIGN KEY (userstoryid) REFERENCES userstory (id)
);