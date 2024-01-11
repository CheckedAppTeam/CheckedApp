import { Outlet, Link, useSearchParams } from "react-router-dom";
import { useState } from "react";

export function ItemListLayout(){
    const [searchParams, setSearchParams] = useSearchParams({ n: 3 })
    const name = searchParams.get("n")

    return (
        <>
        <h1>ItemLists</h1>
        <Link to="/itemlists/1">ItemList1</Link>
        <br />
        <Link to={`/itemlists/${name}`}>ItemList: {name}</Link>
        <br />
        <Link to="/itemlists/new">New ItemList</Link>
        <Outlet context={{ hello: "something"}}/>
        <input type="number" value={name} onChange={e => setSearchParams({ n: e.target.value })}/>
        </>
        )
}