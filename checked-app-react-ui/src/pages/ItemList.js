import { useOutletContext, useParams } from 'react-router-dom'

export function ItemList() {
  const { id } = useParams()
  const obj = useOutletContext()
  return (
    <h1>
      ItemList {id} {obj.hello}
    </h1>
  )
}
