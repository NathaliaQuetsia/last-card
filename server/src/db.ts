import knex from "knex";
export const db = knex({
  client: "sqlite3",
  connection: {
    filename: "./dist/database.db",
  },
  useNullAsDefault: true,
});
